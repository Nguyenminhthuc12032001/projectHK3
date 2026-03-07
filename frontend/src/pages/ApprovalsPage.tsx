import { useEffect, useState } from 'react';
import { EmptyState, Section } from '../components';
import { apiRequest } from '../lib/api';
import { useAuth } from '../state/auth';
import type { PolicyRequest } from '../types';
import { SimpleTable } from './DashboardPage';

export function ApprovalsPage() {
  const { user } = useAuth();
  const [pending, setPending] = useState<PolicyRequest[]>([]);
  const [feedback, setFeedback] = useState('');

  async function load() {
    const data = await apiRequest<PolicyRequest[]>('PolicyApproval/pending');
    setPending(Array.isArray(data) ? data : []);
  }

  useEffect(() => {
    void load().catch(() => setPending([]));
  }, []);

  async function act(policyRequestId: number, action: 'approve' | 'reject') {
    await apiRequest<{ message: string }>(`PolicyApproval/${policyRequestId}/${action}`, {
      method: 'POST',
      body: {
        policyRequestId,
        adminId: user?.id,
        remarks: `${action}d in frontend workspace`,
      },
    });
    setFeedback(`Request ${action}d.`);
    await load();
  }

  return (
    <>
      <Section title="Approval queue" subtitle="Formal approval flow for admins and managers">
        {feedback ? <div className="feedback success">{feedback}</div> : null}
        {pending.length ? <SimpleTable rows={pending} /> : <EmptyState title="Nothing pending" description="No requests are waiting for approval." />}
      </Section>

      {pending.length ? (
        <Section title="Approval actions" subtitle="Direct decision controls">
          <div className="chip-grid">
            {pending.map((item) => (
              <div key={item.id} className="action-card">
                <strong>Request #{item.id}</strong>
                <p>{item.requestType} · Employee {item.employeeId}</p>
                <div className="button-row">
                  <button className="primary-button" onClick={() => void act(item.id, 'approve')}>Approve</button>
                  <button className="ghost-button danger" onClick={() => void act(item.id, 'reject')}>Reject</button>
                </div>
              </div>
            ))}
          </div>
        </Section>
      ) : null}
    </>
  );
}
