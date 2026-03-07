import { useEffect, useState } from 'react';
import { EmptyState, Section } from '../components';
import { apiRequest } from '../lib/api';
import { useAuth } from '../state/auth';
import type { Hospital } from '../types';
import { SimpleTable } from './DashboardPage';

export function HospitalsPage() {
  const { user } = useAuth();
  const [hospitals, setHospitals] = useState<Hospital[]>([]);
  const [feedback, setFeedback] = useState('');

  async function load() {
    const data = await apiRequest<Hospital[]>('Hospital', { auth: false });
    setHospitals(data);
  }

  useEffect(() => {
    void load().catch(() => setHospitals([]));
  }, []);

  async function submit(formData: FormData) {
    await apiRequest<{ id: number }>('Hospital', {
      method: 'POST',
      body: {
        hospitalName: String(formData.get('hospitalName')),
        address: String(formData.get('address')),
        city: String(formData.get('city')),
        contactPhone: String(formData.get('contactPhone')),
        email: String(formData.get('email')),
        registeredOn: String(formData.get('registeredOn')),
      },
    });
    setFeedback('Hospital added.');
    await load();
  }

  return (
    <>
      <Section title="Hospital network" subtitle="Available care providers and contact points">
        {hospitals.length ? <SimpleTable rows={hospitals} /> : <EmptyState title="No hospitals" description="The provider network has no data yet." />}
      </Section>

      {(user?.role === 'Admin' || user?.role === 'Manager') ? (
        <Section title="Add hospital" subtitle="Manage the care network available to policy holders">
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
              Hospital name
              <input name="hospitalName" required />
            </label>
            <label>
              City
              <input name="city" required />
            </label>
            <label className="full-span">
              Address
              <input name="address" required />
            </label>
            <label>
              Contact phone
              <input name="contactPhone" />
            </label>
            <label>
              Email
              <input name="email" type="email" />
            </label>
            <label>
              Registered on
              <input name="registeredOn" type="date" required />
            </label>
            <button className="primary-button" type="submit">Save hospital</button>
          </form>
        </Section>
      ) : null}
    </>
  );
}
