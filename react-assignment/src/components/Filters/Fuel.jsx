// Petrol = 1,
// Diesel = 2,
// Cng = 3,
// Lpg = 4,
// Electric = 5,
// Hybrid = 6


import React from 'react';
import './Fuel.scss';

// import { useFilterContext } from '../../context/FilterContext';
import Checkbox from './CheckBox';
import { useDispatch, useSelector } from 'react-redux';
import { toggleFuelType } from '../../actions/filterAction';



const Fuel = () => {
    // const { filters, updateFilter } = useFilterContext();

    const dispatch = useDispatch();
    const fuelType = useSelector((state) => state.fuelType ?? [] );

    // To make sure fuelType is always an array by initializing with empty array if undefined
    // const fuelTypeArray = filters.fuelType ?? [];

    const fuelOptions = [
        { id: 'petrol', label: 'Petrol', value: 1 },
        { id: 'diesel', label: 'Diesel', value: 2 },
        { id: 'cng', label: 'CNG', value: 3 },
        { id: 'lpg', label: 'LPG', value: 4 },
        { id: 'electric', label: 'Electric', value: 5 },
        { id: 'hybrid', label: 'Hybrid', value: 6 }
    ];

    // const handleCheckboxChange = (fuelValue) => (e) => {
    //     const updatedFuelType = e.target.checked
    //         ? [...fuelTypeArray, fuelValue]  // Add fuel type if checked
    //         : fuelTypeArray.filter((value) => value !== fuelValue); // Remove if unchecked

    //     updateFilter('fuelType', updatedFuelType);
    // };

    const handleChange = (fuelValue) => {
      dispatch(toggleFuelType(fuelValue));

    //   console.log("Fuel Type Filters : " , fuelType )
    };

    return (
        <div className="fuel-container">
            <h2>Fuel Type</h2>
            {fuelOptions.map((fuel) => (
                <Checkbox
                    key={fuel.value}
                    id={fuel.id}
                    name={fuel.id}
                    checked={fuelType.includes(fuel.value)}
                    label={fuel.label}
                    onChange={() => handleChange(fuel.value)}
                />
            ))}
        </div>
    );
};

export default Fuel;
