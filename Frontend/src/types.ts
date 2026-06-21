export type  Product = {
    Id: number;
    Name: String;
    Category: String;
    Price: number;
}
export type ProductsMap = {
  [id: string]: Product;
};