export interface ApiResponse<T> {
  data: T;
  isSuccess: boolean;
  errors: string[];
}

export interface ApiListResponse<T> extends ApiResponse<T[]> {
  count: number;
  totalCount: number;
}
