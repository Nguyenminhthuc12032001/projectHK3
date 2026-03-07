import type { AuthTokens } from '../types';

const TOKENS_KEY = 'projecthk3.tokens';

export function getStoredTokens(): AuthTokens | null {
  const raw = localStorage.getItem(TOKENS_KEY);
  if (!raw) return null;

  try {
    return JSON.parse(raw) as AuthTokens;
  } catch {
    localStorage.removeItem(TOKENS_KEY);
    return null;
  }
}

export function setStoredTokens(tokens: AuthTokens | null) {
  if (!tokens) {
    localStorage.removeItem(TOKENS_KEY);
    return;
  }

  localStorage.setItem(TOKENS_KEY, JSON.stringify(tokens));
}
