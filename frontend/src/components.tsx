import { useMemo, useState, type ReactNode } from 'react';
import { NavLink, Outlet } from 'react-router-dom';
import { useAuth } from './state/auth';
import type { Role } from './types';

export function LoadingScreen() {
  return <div className="center-shell">Loading portal...</div>;
}

export function EmptyState({ title, description }: { title: string; description: string }) {
  return (
    <div className="empty-state">
      <h3>{title}</h3>
      <p>{description}</p>
    </div>
  );
}

export function Section({
  title,
  subtitle,
  action,
  children,
}: {
  title: string;
  subtitle?: string;
  action?: ReactNode;
  children: ReactNode;
}) {
  return (
    <section className="panel">
      <div className="panel-head">
        <div>
          <h2>{title}</h2>
          {subtitle ? <p>{subtitle}</p> : null}
        </div>
        {action}
      </div>
      {children}
    </section>
  );
}

export function MetricCard({
  label,
  value,
  hint,
}: {
  label: string;
  value: string;
  hint: string;
}) {
  return (
    <div className="metric-card">
      <span>{label}</span>
      <strong>{value}</strong>
      <small>{hint}</small>
    </div>
  );
}

export function AppLayout() {
  const { user, signOut } = useAuth();
  const [open, setOpen] = useState(false);
  const navigation = useMemo(() => filterNavigation(user?.role), [user?.role]);

  return (
    <div className="app-shell">
      <aside className={`sidebar ${open ? 'open' : ''}`}>
        <div className="brand">
          <div className="brand-mark">HK3</div>
          <div>
            <strong>ProjectHK3</strong>
            <p>Insurance operations portal</p>
          </div>
        </div>
        <nav>
          {navigation.map((item) => (
            <NavLink
              key={item.to}
              to={item.to}
              className={({ isActive }) => `nav-link ${isActive ? 'active' : ''}`}
              onClick={() => setOpen(false)}
            >
              <span>{item.label}</span>
              <small>{item.caption}</small>
            </NavLink>
          ))}
        </nav>
        <button className="ghost-button full-width" onClick={() => void signOut()}>
          Sign out
        </button>
      </aside>

      <div className="content-shell">
        <header className="topbar">
          <button className="menu-button" onClick={() => setOpen((value) => !value)}>
            Menu
          </button>
          <div className="topbar-copy">
            <h1>Insurance workspace</h1>
            <p>Quiet UI, fast workflows, direct access to backend operations.</p>
          </div>
          <div className="user-chip">
            <strong>{user?.name}</strong>
            <span>{user?.role}</span>
          </div>
        </header>
        <main className="page-grid">
          <Outlet />
        </main>
      </div>
    </div>
  );
}

const baseNavigation = [
  { to: '/', label: 'Overview', caption: 'Summary and recent activity', roles: ['Admin', 'Manager', 'Employee', 'Finance'] as Role[] },
  { to: '/policies', label: 'Policies', caption: 'Catalog and coverage terms', roles: ['Admin', 'Manager', 'Employee'] as Role[] },
  { to: '/insurers', label: 'Insurers', caption: 'Insurance companies', roles: ['Admin', 'Manager'] as Role[] },
  { to: '/hospitals', label: 'Hospitals', caption: 'Provider network', roles: ['Admin', 'Manager', 'Employee', 'Finance'] as Role[] },
  { to: '/requests', label: 'Requests', caption: 'Enrollment and claims', roles: ['Admin', 'Manager', 'Employee'] as Role[] },
  { to: '/approvals', label: 'Approvals', caption: 'Pending request decisions', roles: ['Admin', 'Manager'] as Role[] },
  { to: '/finance', label: 'Finance', caption: 'Disbursement processing', roles: ['Admin', 'Finance'] as Role[] },
  { to: '/notifications', label: 'Notifications', caption: 'Inbox and unread status', roles: ['Admin', 'Manager', 'Employee', 'Finance'] as Role[] },
  { to: '/reports', label: 'Reports', caption: 'Operational analytics', roles: ['Admin'] as Role[] },
];

function filterNavigation(role?: Role) {
  if (!role) return [];
  return baseNavigation.filter((item) => item.roles.includes(role));
}
