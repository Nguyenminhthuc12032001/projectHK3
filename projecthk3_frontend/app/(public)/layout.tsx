import "../globals.css";
import  Header from "../component/Header"
import  Footer from "../component/Footer"
import  PublicNavbar from "../component/PublicNavbar"

export default function PublicLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body >
        
        <PublicNavbar/>
        <main className="min-h-screen">
          {children}
          </main>           
        <Footer/>
      </body>
    </html>
  );
}
