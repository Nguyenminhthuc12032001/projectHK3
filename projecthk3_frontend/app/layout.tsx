import "./globals.css";
import  Header from "./Header"
import HomePage from "./page";


export default function RootLayout({
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
