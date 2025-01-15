export const SET_BUDGET = 'SET_BUDGET';
export const TOGGLE_FUEL_TYPE = 'TOGGLE_FUEL_TYPE';
export const CLEAR_FILTERS = 'CLEAR_FILTERS';
export const SORT_PRODUCTS = 'SORT_PRODUCTS';
export const CLEAR_BUDGET = 'CLEAR_BUDGET'

export const setBudget = (budget) => ({
    type: SET_BUDGET,
    payload: budget
});

export const toggleFuelType = (fuelType) => ({
    type: TOGGLE_FUEL_TYPE,
    payload: fuelType
});

export const setSortType = (sortType) => ({
    type: SORT_PRODUCTS,
    payload: sortType
});

export const clearBudget = () => ({
    type: CLEAR_BUDGET
})

export const clearFilters = () => ({
    type: CLEAR_FILTERS
});
