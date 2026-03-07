import { useEffect, useState } from 'react';
import { EmptyState, Section } from '../components';
import { apiRequest } from '../lib/api';
import { useAuth } from '../state/auth';
import type { Insurer, Policy, PolicyDescription, PolicyDetail } from '../types';
import { SimpleTable } from './DashboardPage';

export function PoliciesPage() {
  const { user } = useAuth();
  const [policies, setPolicies] = useState<Policy[]>([]);
  const [insurers, setInsurers] = useState<Insurer[]>([]);
  const [selectedPolicy, setSelectedPolicy] = useState<number | null>(null);
  const [detail, setDetail] = useState<PolicyDetail | null>(null);
  const [descriptions, setDescriptions] = useState<PolicyDescription[]>([]);
  const [feedback, setFeedback] = useState('');

  async function load() {
    const [policyData, insurerData] = await Promise.all([
      apiRequest<Policy[]>('Policy'),
      apiRequest<Insurer[]>('Insurer'),
    ]);
    setPolicies(policyData);
    setInsurers(insurerData);
    if (!selectedPolicy && policyData[0]) {
      setSelectedPolicy(policyData[0].id);
    }
  }

  useEffect(() => {
    void load().catch(() => {
      setPolicies([]);
      setInsurers([]);
    });
  }, []);

  useEffect(() => {
    if (!selectedPolicy) return;

    void Promise.all([
      apiRequest<PolicyDetail>(`Policy/${selectedPolicy}`),
      apiRequest<PolicyDescription[]>(`policies/${selectedPolicy}/descriptions`),
    ])
      .then(([policyDetail, policyDescriptions]) => {
        setDetail(policyDetail);
        setDescriptions(policyDescriptions);
      })
      .catch(() => {
        setDetail(null);
        setDescriptions([]);
      });
  }, [selectedPolicy]);

  async function createPolicy(formData: FormData) {
    await apiRequest<{ message: string }>('Policy', {
      method: 'POST',
      body: {
        companyId: Number(formData.get('companyId')),
        policyName: String(formData.get('policyName')),
        coverageAmount: Number(formData.get('coverageAmount')),
        premiumAmount: Number(formData.get('premiumAmount')),
        effectiveFrom: String(formData.get('effectiveFrom')),
        effectiveTo: String(formData.get('effectiveTo')),
        status: String(formData.get('status')),
      },
    });
    setFeedback('Policy created successfully.');
    await load();
  }

  async function createDescription(formData: FormData) {
    if (!selectedPolicy) return;

    await apiRequest<number>(`policies/${selectedPolicy}/descriptions`, {
      method: 'POST',
      body: {
        policySummary: String(formData.get('policySummary')),
        benefits: String(formData.get('benefits')),
        exclusions: String(formData.get('exclusions')),
        termsConditions: String(formData.get('termsConditions')),
      },
    });

    setFeedback('Description added.');
    const policyDescriptions = await apiRequest<PolicyDescription[]>(`policies/${selectedPolicy}/descriptions`);
    setDescriptions(policyDescriptions);
  }

  async function removePolicy(policyId: number) {
    await apiRequest<{ message: string }>(`Policy/${policyId}`, { method: 'DELETE' });
    setFeedback('Policy archived.');
    await load();
  }

  return (
    <>
      <Section title="Policy catalog" subtitle="Coverage products exposed by the API">
        {feedback ? <div className="feedback success">{feedback}</div> : null}
        <div className="master-detail">
          <div>
            {policies.length ? (
              <div className="list-stack">
                {policies.map((item) => (
                  <button
                    key={item.id}
                    className={`list-card ${selectedPolicy === item.id ? 'selected' : ''}`}
                    onClick={() => setSelectedPolicy(item.id)}
                  >
                    <strong>{item.policyName}</strong>
                    <span>{item.id}</span>
                    <small>{item.coverageAmount.toLocaleString()} coverage</small>
                  </button>
                ))}
              </div>
            ) : (
              <EmptyState title="No policies yet" description="Create the first policy to populate this space." />
            )}
          </div>

          <div className="detail-column">
            {detail ? (
              <div className="detail-card">
                <h3>{detail.policyName}</h3>
                <p>Status: {detail.status}</p>
                <p>Coverage: {detail.coverageAmount}</p>
                <p>Premium: {detail.premiumAmount}</p>
                <p>Period: {detail.effectiveFrom} to {detail.effectiveTo}</p>
                {(user?.role === 'Admin' || user?.role === 'Manager') ? (
                  <button className="ghost-button" onClick={() => void removePolicy(detail.id)}>
                    Archive policy
                  </button>
                ) : null}
              </div>
            ) : (
              <EmptyState title="Select a policy" description="Detailed information appears here." />
            )}
          </div>
        </div>
      </Section>

      {(user?.role === 'Admin' || user?.role === 'Manager') ? (
        <Section title="Create policy" subtitle="Uses `/api/Policy` and insurer ids as company ids">
          <form
            className="field-grid"
            onSubmit={(event) => {
              event.preventDefault();
              void createPolicy(new FormData(event.currentTarget));
              event.currentTarget.reset();
            }}
          >
            <label>
              Policy name
              <input name="policyName" required />
            </label>
            <label>
              Company
              <select name="companyId" required defaultValue="">
                <option value="" disabled>Select insurer</option>
                {insurers.map((item) => (
                  <option key={item.id} value={item.id}>{item.companyName}</option>
                ))}
              </select>
            </label>
            <label>
              Coverage amount
              <input name="coverageAmount" type="number" min="0" required />
            </label>
            <label>
              Premium amount
              <input name="premiumAmount" type="number" min="0" required />
            </label>
            <label>
              Effective from
              <input name="effectiveFrom" type="date" required />
            </label>
            <label>
              Effective to
              <input name="effectiveTo" type="date" required />
            </label>
            <label>
              Status
              <select name="status" defaultValue="Active">
                <option>Active</option>
                <option>Inactive</option>
              </select>
            </label>
            <button className="primary-button" type="submit">Create policy</button>
          </form>
        </Section>
      ) : null}

      <Section title="Policy descriptions" subtitle="Summary, benefits, exclusions, terms">
        {descriptions.length ? <SimpleTable rows={descriptions} /> : <EmptyState title="No descriptions" description="Add one to make the product understandable." />}
      </Section>

      {(user?.role === 'Admin' || user?.role === 'Manager') && selectedPolicy ? (
        <Section title="Add description" subtitle="Attach detailed terms to the selected policy">
          <form
            className="form-stack"
            onSubmit={(event) => {
              event.preventDefault();
              void createDescription(new FormData(event.currentTarget));
              event.currentTarget.reset();
            }}
          >
            <label>
              Policy summary
              <textarea name="policySummary" required rows={3} />
            </label>
            <label>
              Benefits
              <textarea name="benefits" required rows={3} />
            </label>
            <label>
              Exclusions
              <textarea name="exclusions" required rows={3} />
            </label>
            <label>
              Terms and conditions
              <textarea name="termsConditions" required rows={3} />
            </label>
            <button className="primary-button" type="submit">Save description</button>
          </form>
        </Section>
      ) : null}
    </>
  );
}
