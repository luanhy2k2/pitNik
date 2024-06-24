import { BasePaging } from "../Paging.entity";

export interface BaseQueriesResponse<T> {
    pageIndex: number;
    pageSize: number;
    total: number;
    items: T[] ;
    keyword:string
  }

