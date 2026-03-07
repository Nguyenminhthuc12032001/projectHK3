import { useEffect, useState } from 'react';
import { EmptyState, Section } from '../components';
import { apiRequest } from '../lib/api';
import { useAuth } from '../state/auth';
import type { NotificationItem } from '../types';
import { SimpleTable } from './DashboardPage';

export function NotificationsPage() {
  const { user } = useAuth();
  const [items, setItems] = useState<NotificationItem[]>([]);
  const [feedback, setFeedback] = useState('');

  async function load() {
    if (!user) return;
    const data = await apiRequest<NotificationItem[]>(`Notification/user/${user.id}`);
    setItems(data);
  }

  useEffect(() => {
    void load().catch(() => setItems([]));
  }, [user]);

  async function markRead(id: number) {
    await apiRequest<void>(`Notification/read/${id}`, { method: 'PUT' });
    setFeedback('Notification marked as read.');
    await load();
  }

  async function markAll() {
    if (!user) return;
    await apiRequest<void>(`Notification/read-all/${user.id}`, { method: 'PUT' });
    setFeedback('All notifications marked as read.');
    await load();
  }

  const unread = items.filter((item) => !item.isRead);

  return (
    <>
      <Section title="Notifications" subtitle="Inbox for the signed-in user" action={<button className="ghost-button" onClick={() => void markAll()}>Mark all read</button>}>
        {feedback ? <div className="feedback success">{feedback}</div> : null}
        {items.length ? <SimpleTable rows={items} /> : <EmptyState title="Inbox empty" description="No notification records are available for this account." />}
      </Section>

      {unread.length ? (
        <Section title="Unread items" subtitle="Fast interaction for open alerts">
          <div className="chip-grid">
            {unread.map((item) => (
              <div key={item.id} className="action-card">
                <strong>{item.subject ?? item.type}</strong>
                <p>{item.message ?? 'No message content.'}</p>
                <button className="primary-button" onClick={() => void markRead(item.id)}>Mark read</button>
              </div>
            ))}
          </div>
        </Section>
      ) : null}
    </>
  );
}
