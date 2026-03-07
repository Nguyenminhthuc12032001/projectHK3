import { useEffect, useState } from 'react';
import { EmptyState, Section } from '../components';
import { apiRequest } from '../lib/api';
import type { Insurer } from '../types';
import { SimpleTable } from './DashboardPage';

export function InsurersPage() {
  const [insurers, setInsurers] = useState<Insurer[]>([]);
  const [feedback, setFeedback] = useState('');

  async function load() {
    const data = await apiRequest<Insurer[]>('Insurer');
    setInsurers(data);
  }

  useEffect(() => {
    void load().catch(() => setInsurers([]));
  }, []);

  async function submit(formData: FormData) {
    await apiRequest<number>('Insurer', {
      method: 'POST',
      body: {
        companyName: String(formData.get('companyName')),
        address: String(formData.get('address')),
        contactEmail: String(formData.get('contactEmail')),
        contactPhone: String(formData.get('contactPhone')),
        website: String(formData.get('website')),
      },
    });
    setFeedback('Insurer created.');
    await load();
  }

  return (
    <>
      <Section title="Insurers" subtitle="Company directory backing policies">
        {insurers.length ? <SimpleTable rows={insurers} /> : <EmptyState title="No insurers" description="Create insurer records before creating policies." />}
      </Section>

      <Section title="Add insurer" subtitle="Creates a company record via `/api/Insurer`">
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
            Company name
            <input name="companyName" required />
          </label>
          <label>
            Contact email
            <input name="contactEmail" type="email" required />
          </label>
          <label>
            Contact phone
            <input name="contactPhone" required />
          </label>
          <label>
            Website
            <input name="website" type="url" required />
          </label>
          <label className="full-span">
            Address
            <input name="address" required />
          </label>
          <button className="primary-button" type="submit">Save insurer</button>
        </form>
      </Section>
    </>
  );
}
