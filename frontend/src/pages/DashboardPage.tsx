import { useEffect, useState } from 'react';
import { EmptyState, MetricCard, Section } from '../components';
import { apiRequest } from '../lib/api';
import { useAuth } from '../state/auth';
import type {
  DashboardSummary,
  Disbursement,
  NotificationItem,
  Policy,
  PolicyRequest,
} from '../types';

export function DashboardPage() {
  const { user } = useAuth();
  const [summary, setSummary] = useState<DashboardSummary | null>(null);
  const [policies, setPolicies] = useState<Policy[]>([]);
  const [requests, setRequests] = useState<PolicyRequest[]>([]);
  const [finance, setFinance] = useState<Disbursement[]>([]);
  const [notifications, setNotifications] = useState<NotificationItem[]>([]);

  useEffect(() => {
    if (!user) return;

    void apiRequest<Policy[]>('Policy').then((data) => setPolicies(data.slice(0, 4))).catch(() => setPolicies([]));
    void apiRequest<NotificationItem[]>(`Notification/user/${user.id}`).then((data) => setNotifications(data.slice(0, 4))).catch(() => setNotifications([]));

    if (user.role === 'Admin') {
      void apiRequest<DashboardSummary>('Report/dashboard-summary').then(setSummary).catch(() => setSummary(null));
    }

    if (user.role === 'Employee') {
      void apiRequest<PolicyRequest[]>(`PolicyRequest/employee/${user.id}`).then((data) => setRequests(data.slice(0, 4))).catch(() => setRequests([]));
    } else if (user.role === 'Admin' || user.role === 'Manager') {
      void apiRequest<PolicyRequest[]>('PolicyApproval/pending').then((data) => setRequests(Array.isArray(data) ? data.slice(0, 4) : [])).catch(() => setRequests([]));
    }

    if (user.role === 'Admin' || user.role === 'Finance') {
      void apiRequest<Disbursement[]>('Finance/pending').then((data) => setFinance(data.slice(0, 4))).catch(() => setFinance([]));
    }
  }, [user]);

  return (
    <>
      <section className="hero-strip">
        <div>
          <span className="eyebrow">Workspace</span>
          <h2>{user?.name}, everything important is one layer deep.</h2>
          <p>
            The interface is organized around policy lifecycle, claims handling,
            disbursement processing, and reporting.
          </p>
        </div>
      </section>

      <div className="metrics-grid">
        <MetricCard label="Signed in as" value={user?.role ?? 'Unknown'} hint={user?.email ?? 'No email'} />
        <MetricCard label="Policies visible" value={String(policies.length)} hint="Live from /api/Policy" />
        <MetricCard label="Pending focus" value={String(requests.length)} hint="Requests or approvals in view" />
        <MetricCard label="Unread signals" value={String(notifications.filter((item) => !item.isRead).length)} hint="Latest notifications for current user" />
      </div>

      {summary ? (
        <div className="metrics-grid">
          <MetricCard label="Employees" value={String(summary.totalEmployees)} hint="Total registered employees" />
          <MetricCard label="Active policies" value={String(summary.totalActivePolicies)} hint="Coverage in force" />
          <MetricCard label="Pending claims" value={String(summary.pendingClaims)} hint="Waiting for handling" />
          <MetricCard label="Total disbursed" value={currency(summary.totalDisbursed)} hint="Released capital" />
        </div>
      ) : null}

      <Section title="Policy snapshot" subtitle="Current policy catalog in the backend">
        {policies.length ? <SimpleTable rows={policies} /> : <EmptyState title="No policies" description="Create a policy to start building the catalog." />}
      </Section>

      <Section title="Operational queue" subtitle="Requests, approvals, or claims relevant to your role">
        {requests.length ? <SimpleTable rows={requests} /> : <EmptyState title="Queue is clear" description="There are no immediate requests assigned to this view." />}
      </Section>

      {(user?.role === 'Admin' || user?.role === 'Finance') ? (
        <Section title="Pending disbursements" subtitle="Finance transactions that still require action">
          {finance.length ? <SimpleTable rows={finance} /> : <EmptyState title="No pending disbursements" description="Finance queue is currently empty." />}
        </Section>
      ) : null}

      <Section title="Recent notifications" subtitle="System messages and operational alerts">
        {notifications.length ? <SimpleTable rows={notifications} /> : <EmptyState title="No notifications" description="Your inbox has no records yet." />}
      </Section>
    </>
  );
}

export function SimpleTable<T extends object>({
  rows,
}: {
  rows: T[];
}) {
  const columns = rows.length ? (Object.keys(rows[0]) as Array<keyof T>).slice(0, 5) : [];
  return (
    <div className="table-wrap">
      <table>
        <thead>
          <tr>
            {columns.map((column) => (
              <th key={String(column)}>{String(column)}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {rows.map((row, index) => (
            <tr key={index}>
              {columns.map((column) => (
                <td key={String(column)}>{formatCell(row[column])}</td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export function currency(value: number) {
  return new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
    maximumFractionDigits: 0,
  }).format(value);
}

function formatCell(value: unknown) {
  if (value === null || value === undefined || value === '') return '—';
  if (typeof value === 'number') return Number.isInteger(value) ? String(value) : currency(value);
  if (typeof value === 'boolean') return value ? 'Yes' : 'No';
  return String(value);
}
