import React, { useEffect } from 'react'
import './AppliedFilters.scss'
import { useDispatch, useSelector } from 'react-redux'
import { setSortType,clearBudget } from '../../actions/filterAction';

const AppliedFilters = () => {

  const dispatch = useDispatch();

  const fuelType = useSelector((state) => state.fuelType ?? [] );
  const budget = useSelector((state) => state.budget);
  const sortType = useSelector((state) => state.sortType);

  // Sort Based on product.priceNumeric and product.kmNumeric

  // useEffect(() => {
  //   console.log("sort Type",sortType) 
  // },[sortType])

  return (
    <div className='applied-filter-container'>
        
        {
          ( budget.min !== 0 && budget.max !== Infinity ) &&
          <p className="applied-filters-card flex-container">Budget ₹ {budget.min} - {budget.max} Lakh <i className="fa-solid fa-xmark" onClick={() => dispatch(clearBudget())} ></i> </p>
        }
        
  
        <div className='flex-container' style={{gap: '1rem'}}>
            <p style={{fontSize: '0.9rem', fontWeight: '500', color: '#1d1d1d'}} >Sort By : </p>
            <select 
              name="best-option" 
              id="best-option" 
              className='sorting'
              onChange={(e) => { dispatch(setSortType(e.target.value)); } }
            >
                {/* <option value="best-match">Best Match</option> */}
                <option value="price-lh">Price: Low to High</option>
                <option value="price-hl">Price: High to Low</option>
                <option value="kms-lh">Kms: Low to High</option>
                <option value="kms-hl">Kms: High to Low</option>
            </select>
        </div>

    </div>
  )
}

export default AppliedFilters