import { useEffect, useState } from 'react';
import { EmptyState, Section } from '../components';
import { apiRequest } from '../lib/api';
import { useAuth } from '../state/auth';
import type { Policy, PolicyRequest } from '../types';
import { SimpleTable } from './DashboardPage';

export function RequestsPage() {
  const { user } = useAuth();
  const [requests, setRequests] = useState<PolicyRequest[]>([]);
  const [policies, setPolicies] = useState<Policy[]>([]);
  const [feedback, setFeedback] = useState('');

  async function load() {
    const policyData = await apiRequest<Policy[]>('Policy');
    setPolicies(policyData);

    if (user?.role === 'Employee') {
      const data = await apiRequest<PolicyRequest[]>(`PolicyRequest/employee/${user.id}`);
      setRequests(data);
      return;
    }

    const data = await apiRequest<PolicyRequest[]>('PolicyRequest/pending');
    setRequests(data);
  }

  useEffect(() => {
    if (!user) return;
    void load().catch(() => {
      setPolicies([]);
      setRequests([]);
    });
  }, [user]);

  async function submit(formData: FormData) {
    await apiRequest<{ message: string }>('PolicyRequest', {
      method: 'POST',
      body: {
        employeeId: user?.id,
        policyId: Number(formData.get('policyId')),
        requestType: String(formData.get('requestType')),
        remarks: String(formData.get('remarks')),
      },
    });
    setFeedback('Request submitted.');
    await load();
  }

  async function updateStatus(id: number, status: string) {
    await apiRequest<{ message: string }>(`PolicyRequest/${id}`, {
      method: 'PUT',
      body: { status, remarks: `${status} via dashboard` },
    });
    setFeedback(`Request ${status.toLowerCase()}.`);
    await load();
  }

  return (
    <>
      <Section title="Policy requests" subtitle="Enrollment, claim, and cancel flows">
        {requests.length ? <SimpleTable rows={requests} /> : <EmptyState title="No requests" description="No request records are currently available." />}
      </Section>

      {user?.role === 'Employee' ? (
        <Section title="Create request" subtitle="Employee self-service for enrollment or claim submission">
          {feedback ? <div className="feedback success">{feedback}</div> : null}
          <form
            className="field-grid"
            onSubmit={(event) => {
              event.preventDefault();
              void submit(new FormData(event.currentTarget));
              event.currentTarget.reset();
            }}
          >
            <label>
              Policy
              <select name="policyId" required defaultValue="">
                <option value="" disabled>Select policy</option>
                {policies.map((item) => (
                  <option key={item.id} value={item.id}>{item.policyName}</option>
                ))}
              </select>
            </label>
            <label>
              Request type
              <select name="requestType" defaultValue="Enrollment">
                <option>Enrollment</option>
                <option>Claim</option>
                <option>Cancel</option>
              </select>
            </label>
            <label className="full-span">
              Remarks
              <textarea name="remarks" rows={4} />
            </label>
            <button className="primary-button" type="submit">Submit request</button>
          </form>
        </Section>
      ) : null}

      {(user?.role === 'Admin' || user?.role === 'Manager') && requests.length ? (
        <Section title="Quick actions" subtitle="Shortcut state changes for pending requests">
          <div className="chip-grid">
            {requests.map((item) => (
              <div key={item.id} className="action-card">
                <strong>Request #{item.id}</strong>
                <p>Employee {item.employeeId} · Policy {item.policyId}</p>
                <div className="button-row">
                  <button className="ghost-button" onClick={() => void updateStatus(item.id, 'Approved')}>Mark approved</button>
                  <button className="ghost-button danger" onClick={() => void updateStatus(item.id, 'Rejected')}>Mark rejected</button>
                </div>
              </div>
            ))}
          </div>
        </Section>
      ) : null}
    </>
  );
}
