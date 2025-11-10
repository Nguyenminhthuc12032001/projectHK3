// Nếu không muốn dùng .env thì dùng logic tự động:
const isLocalhost = window.location.hostname === "localhost";

// 🔸 Fake API khi dev, thật khi deploy
export const BASE_URL = isLocalhost
  ? "https://jsonplaceholder.typicode.com"    // Mock
  : "https://api.yourbackend.com/api";        // Backend thật