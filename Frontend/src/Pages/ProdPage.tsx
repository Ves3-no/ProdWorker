import { useParams } from "react-router" 
import Get from "../functions/Get";
import type { Product } from "../types";
import { useEffect, useState } from "react";
function ProdPage() {
    const { urlid } = useParams();
    const [prod, setProd] = useState<Product>();
    const id = Number(urlid)
    useEffect(()=> {
        async function Getfunc() {
            const Produ = await Get({id}) 
            .then(function(Produ){return Produ})
        }
        setProd(Getfunc())
    }, [])
    return (
        <>

        </>
    )
}

export default ProdPage
