import { getStoredTokens, setStoredTokens } from './storage';
import type { AuthTokens } from '../types';

const API_URL = import.meta.env.VITE_API_URL ?? 'http://localhost:5191/api';

type Method = 'GET' | 'POST' | 'PUT' | 'PATCH' | 'DELETE';

interface RequestOptions {
  method?: Method;
  body?: unknown;
  auth?: boolean;
  query?: Record<string, string | number | boolean | undefined | null>;
}

let refreshPromise: Promise<AuthTokens | null> | null = null;

function buildUrl(path: string, query?: RequestOptions['query']) {
  const url = new URL(`${API_URL}/${path.replace(/^\//, '')}`);
  Object.entries(query ?? {}).forEach(([key, value]) => {
    if (value !== undefined && value !== null && value !== '') {
      url.searchParams.set(key, String(value));
    }
  });
  return url.toString();
}

async function refreshTokens() {
  if (refreshPromise) return refreshPromise;

  const current = getStoredTokens();
  if (!current?.refreshToken) return null;

  refreshPromise = fetch(buildUrl('Auth/refresh'), {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ refreshToken: current.refreshToken }),
  })
    .then(async (response) => {
      if (!response.ok) {
        setStoredTokens(null);
        return null;
      }

      const tokens = (await response.json()) as AuthTokens;
      setStoredTokens(tokens);
      return tokens;
    })
    .finally(() => {
      refreshPromise = null;
    });

  return refreshPromise;
}

export async function apiRequest<T>(path: string, options: RequestOptions = {}): Promise<T> {
  const { method = 'GET', body, auth = true, query } = options;
  const tokens = getStoredTokens();
  const headers: Record<string, string> = {};

  if (body !== undefined) {
    headers['Content-Type'] = 'application/json';
  }

  if (auth && tokens?.accessToken) {
    headers.Authorization = `Bearer ${tokens.accessToken}`;
  }

  const response = await fetch(buildUrl(path, query), {
    method,
    headers,
    body: body !== undefined ? JSON.stringify(body) : undefined,
  });

  if (response.status === 401 && auth && path !== 'Auth/refresh') {
    const renewed = await refreshTokens();
    if (renewed?.accessToken) {
      return apiRequest<T>(path, options);
    }
  }

  if (!response.ok) {
    const text = await response.text();
    throw new Error(text || `Request failed: ${response.status}`);
  }

  if (response.status === 204) {
    return undefined as T;
  }

  return (await response.json()) as T;
}
