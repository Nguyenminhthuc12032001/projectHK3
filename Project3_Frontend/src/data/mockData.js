// Dữ liệu mock dùng test cho Admin & Employee
export const mockUsers = [
  { id: 1, name: "Alice Johnson", role: "employee", email: "alice@example.com" },
  { id: 2, name: "Bob Smith", role: "employee", email: "bob@example.com" },
  { id: 3, name: "Charlie Brown", role: "admin", email: "charlie@example.com" },
  { id: 4, name: "David Lee", role: "employee", email: "david@example.com" },
  { id: 5, name: "Evelyn Miller", role: "employee", email: "evelyn@example.com" },
];

export const mockTasks = [
  { id: 1, title: "Submit project report", done: false, assignedTo: 1 },
  { id: 2, title: "Attend meeting", done: true, assignedTo: 2 },
  { id: 3, title: "Review UI design", done: false, assignedTo: 1 },
  { id: 4, title: "Prepare team training", done: false, assignedTo: 2 },
  { id: 5, title: "Fix login issue", done: true, assignedTo: 4 },
];
