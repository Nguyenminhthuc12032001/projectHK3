export type Role = 'Admin' | 'Manager' | 'Employee' | 'Finance';

export interface AuthTokens {
  accessToken: string;
  refreshToken: string;
  expireAt: string;
  tokenType: string;
}

export interface UserInfo {
  id: number;
  email: string;
  name: string;
  phone?: string | null;
  role: Role;
}

export interface Policy {
  id: number;
  policyName: string;
  coverageAmount: number;
  premiumAmount: number;
}

export interface PolicyDetail extends Policy {
  effectiveFrom: string;
  effectiveTo: string;
  status: string;
}

export interface PolicyDescription {
  id: number;
  policySummary: string;
  benefits: string;
  exclusions: string;
  termsConditions: string;
}

export interface Insurer {
  id: number;
  companyName: string;
  address: string;
  contactEmail: string;
  contactPhone: string;
  website: string;
}

export interface Hospital {
  id: number;
  hospitalName?: string | null;
  address?: string | null;
  city?: string | null;
  contactPhone?: string | null;
  email?: string | null;
}

export interface PolicyRequest {
  id: number;
  employeeId: number;
  policyId: number;
  requestType: string;
  requestDate: string;
  status: string;
  remarks?: string | null;
}

export interface Disbursement {
  id: number;
  approvalId: number;
  amount: number;
  method: string;
  status: string;
  createdAt: string;
}

export interface NotificationItem {
  id: number;
  recipient?: string | null;
  type: string;
  subject?: string | null;
  message?: string | null;
  isRead: boolean;
  status: string;
  createdAt: string;
}

export interface DashboardSummary {
  totalEmployees: number;
  totalActivePolicies: number;
  pendingClaims: number;
  totalDisbursed: number;
}

export interface ClaimReportItem {
  id: number;
  employeeId: number;
  requestDate: string;
  status: string;
  remarks?: string | null;
  policyId: number;
}

export interface GenericReportItem {
  [key: string]: string | number | null;
}
