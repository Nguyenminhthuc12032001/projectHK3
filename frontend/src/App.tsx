import type { ReactNode } from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import { AppLayout, LoadingScreen } from './components';
import { useAuth } from './state/auth';
import type { Role } from './types';
import { ApprovalsPage } from './pages/ApprovalsPage';
import { AuthPage } from './pages/AuthPage';
import { DashboardPage } from './pages/DashboardPage';
import { FinancePage } from './pages/FinancePage';
import { HospitalsPage } from './pages/HospitalsPage';
import { InsurersPage } from './pages/InsurersPage';
import { NotificationsPage } from './pages/NotificationsPage';
import { PoliciesPage } from './pages/PoliciesPage';
import { ReportsPage } from './pages/ReportsPage';
import { RequestsPage } from './pages/RequestsPage';

export function App() {
  const { loading, user } = useAuth();

  if (loading) return <LoadingScreen />;

  return (
    <Routes>
      <Route path="/auth" element={user ? <Navigate to="/" replace /> : <AuthPage />} />
      <Route path="/" element={user ? <AppLayout /> : <Navigate to="/auth" replace />}>
        <Route index element={<DashboardPage />} />
        <Route path="policies" element={<RoleGuard allow={['Admin', 'Manager', 'Employee']}><PoliciesPage /></RoleGuard>} />
        <Route path="insurers" element={<RoleGuard allow={['Admin', 'Manager']}><InsurersPage /></RoleGuard>} />
        <Route path="hospitals" element={<HospitalsPage />} />
        <Route path="requests" element={<RoleGuard allow={['Admin', 'Manager', 'Employee']}><RequestsPage /></RoleGuard>} />
        <Route path="approvals" element={<RoleGuard allow={['Admin', 'Manager']}><ApprovalsPage /></RoleGuard>} />
        <Route path="finance" element={<RoleGuard allow={['Admin', 'Finance']}><FinancePage /></RoleGuard>} />
        <Route path="notifications" element={<NotificationsPage />} />
        <Route path="reports" element={<RoleGuard allow={['Admin']}><ReportsPage /></RoleGuard>} />
      </Route>
      <Route path="*" element={<Navigate to={user ? '/' : '/auth'} replace />} />
    </Routes>
  );
}

function RoleGuard({
  allow,
  children,
}: {
  allow: Role[];
  children: ReactNode;
}) {
  const { user } = useAuth();
  if (!user) return <Navigate to="/auth" replace />;
  if (!allow.includes(user.role)) return <Navigate to="/" replace />;
  return <>{children}</>;
}
