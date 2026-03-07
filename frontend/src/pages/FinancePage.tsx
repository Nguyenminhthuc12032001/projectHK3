import { useEffect, useState } from 'react';
import { EmptyState, Section } from '../components';
import { apiRequest } from '../lib/api';
import { useAuth } from '../state/auth';
import type { Disbursement } from '../types';
import { SimpleTable } from './DashboardPage';

export function FinancePage() {
  const { user } = useAuth();
  const [items, setItems] = useState<Disbursement[]>([]);
  const [feedback, setFeedback] = useState('');

  async function load() {
    const data = await apiRequest<Disbursement[]>('Finance/pending');
    setItems(data);
  }

  useEffect(() => {
    void load().catch(() => setItems([]));
  }, []);

  async function process(id: number, action: 'approve' | 'execute' | 'reject') {
    await apiRequest<string>(`Finance/${id}/${action}`, {
      method: 'PUT',
      query: action === 'reject' ? { remarks: 'Rejected in frontend workspace' } : undefined,
    });
    setFeedback(`Transaction ${action}d.`);
    await load();
  }

  return (
    <>
      <Section title="Disbursement queue" subtitle="Pending financial transactions from approved claims">
        {feedback ? <div className="feedback success">{feedback}</div> : null}
        {items.length ? <SimpleTable rows={items} /> : <EmptyState title="No transactions" description="There are no finance tasks waiting right now." />}
      </Section>

      {items.length ? (
        <Section title="Finance actions" subtitle={user?.role === 'Finance' ? 'Execute approved payments.' : 'Approve, reject, or execute disbursements.'}>
          <div className="chip-grid">
            {items.map((item) => (
              <div key={item.id} className="action-card">
                <strong>Transaction #{item.id}</strong>
                <p>{item.status} · {item.method} · {item.amount}</p>
                <div className="button-row">
                  {user?.role === 'Admin' ? (
                    <>
                      <button className="ghost-button" onClick={() => void process(item.id, 'approve')}>Approve</button>
                      <button className="ghost-button danger" onClick={() => void process(item.id, 'reject')}>Reject</button>
                    </>
                  ) : null}
                  <button className="primary-button" onClick={() => void process(item.id, 'execute')}>Execute</button>
                </div>
              </div>
            ))}
          </div>
        </Section>
      ) : null}
    </>
  );
}
