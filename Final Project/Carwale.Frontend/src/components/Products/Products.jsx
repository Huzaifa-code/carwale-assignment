import React, { useCallback, useEffect, useState } from 'react'
import './Products.scss'
import AppliedFilters from '../AppliedFilters/AppliedFilters';
import { useSelector } from 'react-redux';
import { fetchProducts } from '../../utils/fetchProducts';

const Products = () => {

    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(false);

    // Filters - Fuel Type and Budget .
    // we need to update products based on fuelType and budget
    const fuelType = useSelector((state) => state.fuelType ?? [] );
    const budget = useSelector((state) => state.budget);
    const sortType = useSelector((state) => state.sortType);

    // Fetch products with filters
    const fetchProducts = useCallback(async () => {
        setLoading(true);

        try {
            const productsData = await fetchProducts(fuelType, budget, sortType);
            setProducts(productsData);
        } catch (error) {
            console.error('Error fetching products:', error);
        } finally {
            setLoading(false);
        }

        // Convert fuelType array to a string (e.g., "1+2+3")
        // const fuelParam = fuelType.length > 0 ? fuelType.join('+') : '';
        // const budgetParam = budget ? `${budget.min}-${budget.max ?? ''}` : '';

        // const url = `https://stg.carwale.com/api/stocks?fuel=${fuelParam}&budget=${budgetParam}`;
        // // console.log("Fuel Params : ", fuelParam)
        // // console.log("Budget Params : ", budgetParam)

        // try {
        //     const response = await fetch(url);
        //     const data = await response.json();
        //     // console.log("Carwale data : " , data.stocks)

        //     let sortedData = data.stocks; // initiall unsorted

        //     // console.log( "Sort Type : " , sortType)
        //     if(sortType){
        //         switch (sortType) {
        //             case 'price-lh':
        //                 sortedData.sort((a, b) => a.priceNumeric - b.priceNumeric) // ascending order
        //                 break;
        //             case 'price-hl':
        //                 sortedData.sort((a, b) => b.priceNumeric - a.priceNumeric) // descending order
        //                 break;
        //             case 'kms-lh':
        //                 sortedData.sort((a, b) => a.kmNumeric - b.kmNumeric) // ascending order based on kms
        //                 break;
        //             case 'kms-hl':
        //                 sortedData.sort((a, b) => b.kmNumeric - a.kmNumeric) // descending order based on kms
        //                 break;
        //             default:
        //                 break;
        //         }

        //         if(sortType === 'price-lh'){
                    
        //         }
        //     }

        //     setProducts(sortedData || []);
        // } catch (error) {
        //     console.error('Failed to fetch products:', error);
        // } finally {
        //     setLoading(false);
        // }
    }, [fuelType, budget, sortType]);


    useEffect(() => {
        // const fetchAllCars = async () => {
        //     const carwale = await fetch('https://stg.carwale.com/api/stocks')
        //     const carwaleData = await carwale.json();
        //     setProducts(carwaleData.stocks);
        //     // console.log("Carwale data : " , carwaleData.stocks)
        // }
        // fetchAllCars();
        fetchProducts();
    },[fetchProducts])




  return (
    <div>
        <div className='products' >
            {
                loading ? (
                    <div className="loader-wrapper">
                        <img src={'./loader.gif'} alt="Loading..." className="loader" />
                    </div>
                ) : (
                    products.map((product, idx) => {
                        return (
                            <div key={idx} className='card'>
                                <div className='img-wrapper'>
                                    {
                                        (product.imageUrl || product.stockImages[0]) ? (
                                            <img height={250} src={product.imageUrl || product.stockImages[0]} alt={product.carName}  />                        
                                        ) : (
                                            <div style={{backgroundColor: '#1e1e1e', height: 250, width: '100%'}}></div>
                                        )
                                    }
                                </div>
                                <div className='content-wrapper'>
                                    <h3 className='car-name' >{product.carName}</h3>
                                    <p> {product.km} km | {product.fuel}  | {product.cityName}</p>
                                    <h2>RS {product.price}</h2>
                                    <button className='product-btn'>Get Seller Details</button>
                                </div>
                            </div>
                        )
                    })
                )
            }
        </div>
    </div>
  )
}

export default Products