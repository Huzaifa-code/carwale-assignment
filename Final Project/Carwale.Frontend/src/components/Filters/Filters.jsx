import React, { useState } from 'react'
import './Filters.scss'
import Budget from './Budget'
import { useFilterContext } from '../../context/FilterContext'
import Checkbox from './CheckBox'
import Fuel from './Fuel'
import { useDispatch } from 'react-redux'
import { clearFilters } from '../../actions/filterAction'

const Filters = () => {
  const dispatch = useDispatch();

  // I am not actually using context api 
  const {filters, updateFilter} = useFilterContext();

  const filterOptions = [
    { id: 'absure', label: 'CarWale abSure' },
    { id: 'cert', label: 'Certified Cars' },
    { id: 'qravailable', label: 'Quality Report Available' },
    { id: 'luxury', label: 'Luxury Cars' }
  ];

  const handleCheckboxChange = (name) => (e) => {
    updateFilter('carType', { ...filters.carType, [name]: e.target.checked });

    // console.log( "Filters : " ,filters)
  };

  return (
    <div className='filter-container hide-scrollbar'>
        <div style={{display: 'flex', justifyContent: 'space-between', alignItems: 'center'}}>
            <h3 className='flex-container'> <i className="fa-solid fa-filter"></i>  Filters</h3>
            <p 
              onClick={() => dispatch(clearFilters())} 
              style={{cursor: 'pointer', color: 'blue', fontSize: '0.8rem',fontWeight: 'bold'}}
            >
                Clear All
            </p>
        </div>

        
        <div style={{marginBottom: '10px'}} >

          <Budget/>
          <Fuel/>

          {/* 
          This are static filters not actually used 
          For this i have used context API for the rest of filters - budget and fueltype i have used redux          */}
          {/* {filterOptions.map((filter) => (
            <Checkbox
              key={filter.id}
              id={filter.id}
              name={filter.id}
              value={filters.carType[filter.id]}
              label={filter.label}
              onChange={handleCheckboxChange(filter.id)}
            />
          ))} */}
        </div>
    </div>
  )
}

export default Filters