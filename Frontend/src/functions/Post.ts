import type { Product } from "../types"
export default async function Post({Prod}: {Prod: Product}){
    const API_KEY = import.meta.env.VITE_API_KEY
    const API_URL = import.meta.env.VITE_API_URL
    
    const response = await fetch(API_URL, {
        method: "GET",
        headers: {
            "My-Custom-Header": API_KEY,
            "Content-Type": "application/json"
        },
        body: JSON.stringify(Prod)
    })
    if(!response.ok){
        throw new Error("HTTP error: " + response.status);
    }
    return true
}
