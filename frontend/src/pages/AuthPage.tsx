import { useEffect, useState, type ReactNode } from 'react';
import { useNavigate } from 'react-router-dom';
import { apiRequest } from '../lib/api';
import { useAuth } from '../state/auth';
import type { Insurer } from '../types';

export function AuthPage() {
  const { signIn, register } = useAuth();
  const navigate = useNavigate();
  const [mode, setMode] = useState<'login' | 'register' | 'recovery'>('login');
  const [insurers, setInsurers] = useState<Insurer[]>([]);
  const [error, setError] = useState('');
  const [message, setMessage] = useState('');
  const [busy, setBusy] = useState(false);

  useEffect(() => {
    void apiRequest<Insurer[]>('Insurer', { auth: false })
      .then(setInsurers)
      .catch(() => setInsurers([]));
  }, []);

  async function handleLogin(formData: FormData) {
    setBusy(true);
    setError('');
    try {
      await signIn(String(formData.get('email')), String(formData.get('password')));
      navigate('/');
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Unable to sign in.');
    } finally {
      setBusy(false);
    }
  }

  async function handleRegister(formData: FormData) {
    setBusy(true);
    setError('');
    setMessage('');
    try {
      const feedback = await register({
        fullName: String(formData.get('fullName')),
        email: String(formData.get('email')),
        phone: String(formData.get('phone')),
        password: String(formData.get('password')),
        department: String(formData.get('department')),
        hireDate: String(formData.get('hireDate')),
        companyId: Number(formData.get('companyId')),
      });
      setMessage(feedback);
      setMode('login');
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Unable to register.');
    } finally {
      setBusy(false);
    }
  }

  async function handleRecovery(formData: FormData) {
    setBusy(true);
    setError('');
    setMessage('');
    try {
      const response = await apiRequest<{ message: string }>('Auth/forgot-password', {
        method: 'POST',
        auth: false,
        query: { email: String(formData.get('email')) },
      });
      setMessage(response.message);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Unable to send recovery email.');
    } finally {
      setBusy(false);
    }
  }

  return (
    <div className="auth-shell">
      <div className="auth-hero">
        <span className="eyebrow">ProjectHK3</span>
        <h1>Insurance administration, reduced to the essentials.</h1>
        <p>
          A production-ready React workspace for policy operations, approvals,
          hospital networks, finance flows, and reporting.
        </p>
      </div>

      <div className="auth-card panel">
        <div className="segment">
          <button className={mode === 'login' ? 'active' : ''} onClick={() => setMode('login')}>Login</button>
          <button className={mode === 'register' ? 'active' : ''} onClick={() => setMode('register')}>Register</button>
          <button className={mode === 'recovery' ? 'active' : ''} onClick={() => setMode('recovery')}>Reset</button>
        </div>

        {error ? <div className="feedback error">{error}</div> : null}
        {message ? <div className="feedback success">{message}</div> : null}

        {mode === 'login' ? (
          <AuthForm submitLabel={busy ? 'Signing in...' : 'Sign in'} onSubmit={handleLogin}>
            <label>
              Email
              <input name="email" type="email" required placeholder="admin@company.com" />
            </label>
            <label>
              Password
              <input name="password" type="password" required placeholder="••••••••" />
            </label>
          </AuthForm>
        ) : null}

        {mode === 'register' ? (
          <AuthForm submitLabel={busy ? 'Creating account...' : 'Create employee account'} onSubmit={handleRegister}>
            <div className="field-grid">
              <label>
                Full name
                <input name="fullName" required />
              </label>
              <label>
                Work email
                <input name="email" type="email" required />
              </label>
              <label>
                Phone
                <input name="phone" required />
              </label>
              <label>
                Department
                <input name="department" required />
              </label>
              <label>
                Hire date
                <input name="hireDate" type="date" required />
              </label>
              {insurers.length ? (
                <label>
                  Insurer / company
                  <select name="companyId" required defaultValue="">
                    <option value="" disabled>Select company</option>
                    {insurers.map((item) => (
                      <option key={item.id} value={item.id}>{item.companyName}</option>
                    ))}
                  </select>
                </label>
              ) : (
                <label>
                  Company ID
                  <input name="companyId" type="number" min="1" required placeholder="Enter company id" />
                </label>
              )}
            </div>
            <label>
              Password
              <input name="password" type="password" required minLength={8} />
            </label>
          </AuthForm>
        ) : null}

        {mode === 'recovery' ? (
          <AuthForm submitLabel={busy ? 'Sending...' : 'Send reset link'} onSubmit={handleRecovery}>
            <label>
              Email
              <input name="email" type="email" required />
            </label>
          </AuthForm>
        ) : null}
      </div>
    </div>
  );
}

function AuthForm({
  submitLabel,
  onSubmit,
  children,
}: {
  submitLabel: string;
  onSubmit: (formData: FormData) => Promise<void>;
  children: ReactNode;
}) {
  return (
    <form
      className="form-stack"
      onSubmit={(event) => {
        event.preventDefault();
        void onSubmit(new FormData(event.currentTarget));
      }}
    >
      {children}
      <button className="primary-button" type="submit">{submitLabel}</button>
    </form>
  );
}
