import { useEffect, useState } from 'react';
import { EmptyState, MetricCard, Section } from '../components';
import { apiRequest } from '../lib/api';
import type { ClaimReportItem, DashboardSummary, GenericReportItem } from '../types';
import { SimpleTable, currency } from './DashboardPage';

const defaultRange = {
  from: '2025-01-01',
  to: '2026-12-31',
};

export function ReportsPage() {
  const [summary, setSummary] = useState<DashboardSummary | null>(null);
  const [claims, setClaims] = useState<ClaimReportItem[]>([]);
  const [financial, setFinancial] = useState<GenericReportItem[]>([]);
  const [policy, setPolicy] = useState<GenericReportItem[]>([]);
  const [audit, setAudit] = useState<GenericReportItem[]>([]);
  const [range, setRange] = useState(defaultRange);

  async function load(activeRange = range) {
    const [summaryData, claimData, financialData, policyData, auditData] = await Promise.all([
      apiRequest<DashboardSummary>('Report/dashboard-summary'),
      apiRequest<ClaimReportItem[]>('Report/claims', { query: activeRange }),
      apiRequest<GenericReportItem[]>('Report/financial', { query: activeRange }),
      apiRequest<GenericReportItem[]>('Report/policy', { query: activeRange }),
      apiRequest<GenericReportItem[]>('Report/audit', { query: activeRange }),
    ]);

    setSummary(summaryData);
    setClaims(claimData);
    setFinancial(financialData);
    setPolicy(policyData);
    setAudit(auditData);
  }

  useEffect(() => {
    void load().catch(() => {
      setSummary(null);
      setClaims([]);
      setFinancial([]);
      setPolicy([]);
      setAudit([]);
    });
  }, []);

  return (
    <>
      <Section title="Report controls" subtitle="Admin-only analytics from report endpoints">
        <form
          className="field-grid"
          onSubmit={(event) => {
            event.preventDefault();
            const next = {
              from: String(new FormData(event.currentTarget).get('from')),
              to: String(new FormData(event.currentTarget).get('to')),
            };
            setRange(next);
            void load(next);
          }}
        >
          <label>
            From
            <input name="from" type="date" defaultValue={range.from} />
          </label>
          <label>
            To
            <input name="to" type="date" defaultValue={range.to} />
          </label>
          <button className="primary-button" type="submit">Refresh reports</button>
        </form>
      </Section>

      {summary ? (
        <div className="metrics-grid">
          <MetricCard label="Employees" value={String(summary.totalEmployees)} hint="Total users covered" />
          <MetricCard label="Active policies" value={String(summary.totalActivePolicies)} hint="Current active coverage" />
          <MetricCard label="Pending claims" value={String(summary.pendingClaims)} hint="Waiting for completion" />
          <MetricCard label="Disbursed" value={currency(summary.totalDisbursed)} hint="Across recorded transactions" />
        </div>
      ) : null}

      <Section title="Claim report" subtitle={`Range ${range.from} to ${range.to}`}>
        {claims.length ? <SimpleTable rows={claims} /> : <EmptyState title="No claim data" description="No claims match the selected date range." />}
      </Section>

      <Section title="Financial report" subtitle="Capital movement and payment activity">
        {financial.length ? <SimpleTable rows={financial} /> : <EmptyState title="No financial data" description="The backend returned no financial report rows." />}
      </Section>

      <Section title="Policy report" subtitle="Policy analytics over the selected range">
        {policy.length ? <SimpleTable rows={policy} /> : <EmptyState title="No policy report" description="No policy analytics were returned." />}
      </Section>

      <Section title="Audit report" subtitle="System audit events exposed by the API">
        {audit.length ? <SimpleTable rows={audit} /> : <EmptyState title="No audit data" description="Audit report is empty for the selected period." />}
      </Section>
    </>
  );
}
