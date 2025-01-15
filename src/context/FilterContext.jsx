// I am not using this in filters , initially i created this for state management but now i am using Redux for Global State Managment 

import { createContext, useContext, useState } from "react";

const FilterContext = createContext();

export const FilterProvider = ({children}) => {
    const [filters, setFilters] = useState({
        carType: {
            absure: false,
            cert: false,
            qravailable: false,
            luxury: false
        },
        city: '',
        search: '',
        category: '',
        priceRange: [0,3],
        fuel: ''
    });

    const updateFilter = (key, value) => {
        setFilters((prev) => ({...prev, [key]: value}));
    };

    return (
        <FilterContext.Provider value={{filters, updateFilter}} >
            {children}
        </FilterContext.Provider>
    )
}

export const useFilterContext = () => useContext(FilterContext);