import { Outlet } from 'react-router-dom';

const PublicLayout = () => {
  return (
    <div>
      <header>Public Header</header>
      <nav>Public Menu</nav>
      <main>
        <Outlet />
      </main>
    </div>
  );
};

export default PublicLayout;