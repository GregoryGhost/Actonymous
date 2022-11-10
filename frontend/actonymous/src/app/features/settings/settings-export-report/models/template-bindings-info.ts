import { TuiFileLike } from "@taiga-ui/kit";

export { TemplateBindingsInfo, CustomerInfo, ExecutorInfo, ContractInfo };

interface CompanyInfo {
  companyName: string;
  headerFullname: string;
  headerPosition: string;
}

interface CustomerInfo extends CompanyInfo {}

interface ExecutorInfo extends CompanyInfo {
  ratePerHour: number;
}

interface ContractInfo {
  contractNumber: string;
  approvalDate: Date;
  contractFile: TuiFileLike | null;
}

interface TemplateBindingsInfo {
  customerInfo: CustomerInfo;
  executorInfo: ExecutorInfo;
  contractInfo: ContractInfo;
  actTemplateFile: TuiFileLike | null;
  taskTemplateFile: TuiFileLike | null;
}
