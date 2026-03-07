import {
  createContext,
  useContext,
  useEffect,
  useState,
  type PropsWithChildren,
} from 'react';
import { apiRequest } from '../lib/api';
import { getStoredTokens, setStoredTokens } from '../lib/storage';
import type { AuthTokens, UserInfo } from '../types';

interface AuthContextValue {
  tokens: AuthTokens | null;
  user: UserInfo | null;
  loading: boolean;
  signIn: (email: string, password: string) => Promise<void>;
  register: (payload: {
    fullName: string;
    email: string;
    phone: string;
    password: string;
    department: string;
    hireDate: string;
    companyId: number;
  }) => Promise<string>;
  signOut: () => Promise<void>;
}

const AuthContext = createContext<AuthContextValue | undefined>(undefined);

export function AuthProvider({ children }: PropsWithChildren) {
  const [tokens, setTokens] = useState<AuthTokens | null>(() => getStoredTokens());
  const [user, setUser] = useState<UserInfo | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function bootstrap() {
      if (!getStoredTokens()) {
        setLoading(false);
        return;
      }

      try {
        const current = await apiRequest<UserInfo>('Auth/current');
        setUser(current);
      } catch {
        setStoredTokens(null);
        setTokens(null);
        setUser(null);
      } finally {
        setLoading(false);
      }
    }

    void bootstrap();
  }, []);

  async function signIn(email: string, password: string) {
    const result = await apiRequest<AuthTokens>('Auth/login', {
      method: 'POST',
      auth: false,
      body: { email, password },
    });

    setStoredTokens(result);
    setTokens(result);
    const current = await apiRequest<UserInfo>('Auth/current');
    setUser(current);
  }

  async function register(payload: {
    fullName: string;
    email: string;
    phone: string;
    password: string;
    department: string;
    hireDate: string;
    companyId: number;
  }) {
    const response = await apiRequest<{ message: string }>('Auth/register', {
      method: 'POST',
      auth: false,
      body: payload,
    });

    return response.message;
  }

  async function signOut() {
    const current = getStoredTokens();
    try {
      if (current?.refreshToken) {
        await apiRequest<{ message: string }>('Auth/logout', {
          method: 'POST',
          query: { refreshToken: current.refreshToken },
        });
      }
    } catch {
      // Clear local state regardless.
    } finally {
      setStoredTokens(null);
      setTokens(null);
      setUser(null);
    }
  }

  return (
    <AuthContext.Provider value={{ tokens, user, loading, signIn, register, signOut }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const value = useContext(AuthContext);
  if (!value) {
    throw new Error('useAuth must be used inside AuthProvider');
  }
  return value;
}
