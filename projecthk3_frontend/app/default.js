import PublicLayout from "./(public)/layout"

export default function DefaultPage() {
  // Khi vào / (root) thì render Public layout
  return (
    <PublicLayout>
      <HomePage />
    </PublicLayout>
  );
}