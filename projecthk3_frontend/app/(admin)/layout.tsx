
import "../globals.css";
import  Header from "../component/Header"

export default function AdminLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body >
        <Header/> 
        <main>{children}</main>
      </body>
    </html>
  );
}
