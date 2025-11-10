import axios from "axios";
import { toast } from "react-toastify";
import { BASE_URL } from "./apiConfig"; // tách ra cho gọn, có thể hardcode luôn

const axiosClient = axios.create({
  baseURL: BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
  timeout: 10000, // 10 giây
});

// 🔐 Gắn token tự động
axiosClient.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
  },
  (error) => Promise.reject(error)
);

// ⚙️ Xử lý lỗi response
axiosClient.interceptors.response.use(
  (response) => response.data,
  (error) => {
    if (error.response) {
      const msg =
        error.response.data?.message ||
        error.response.data?.Message ||
        "Lỗi máy chủ";
      toast.error(msg);
      throw new Error(msg);
    } else {
      toast.error("Mất kết nối đến server");
      throw new Error(error.message);
    }
  }
);

export default axiosClient;
