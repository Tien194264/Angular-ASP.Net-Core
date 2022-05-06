export interface UpdatePostRequest {
  title: string | undefined;
  content: string | undefined;
  summary: string | undefined;
  urlHandle: string | undefined;
  author: string | undefined;
  visible: string | undefined;
  pushlishDate: Date | undefined;
  updatedDate: Date | undefined;
  featureImageUrl: string | undefined;
}
