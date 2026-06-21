import type { ProductsMap, Product } from "../types";
export default async function Get({id}: {id: undefined | number}){
    var results: ProductsMap | Product | undefined = undefined 
    const API_KEY = import.meta.env.VITE_API_KEY
    const API_URL = import.meta.env.VITE_API_URL
    if(id || id === 0){
        const  NEW_URL = `${API_URL}/${id}`
        const response = await fetch(NEW_URL, {
            method: "GET",
            headers: {
                "My-Custom-Header": API_KEY,
            }
        })
        if(!response.ok){
             throw new Error("HTTP error: " + response.status);
        }
        const data = await response.json() as Product;
        results = data
    } else{
        const response = await fetch(API_URL, {
            method: "GET",
            headers: {
                "My-Custom-Header": API_KEY
            }
        })
        if(!response.ok){
             throw new Error("HTTP error: " + response.status);
        }
        const data = await response.json() as ProductsMap;
        results = data
    }
    return results
}