
import "../globals.css";
import  Header from "../component/Header"

export default function EmployeeLayout({
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
