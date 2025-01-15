import { SET_BUDGET, TOGGLE_FUEL_TYPE, CLEAR_FILTERS, SORT_PRODUCTS, CLEAR_BUDGET } from '../actions/filterAction.js';

const initialState = {
    budget: {min: 0, max: Infinity},
    fuelType: [], // Array to store selected fuel types as integers
    sortType: '' // sorting based on price and kms - price-lh , price-hl, kms-lh, kms-hl
};

const filterReducer = (state = initialState, action) => {
    switch (action.type) {
        case SET_BUDGET:
            return { ...state, budget: action.payload };

        case TOGGLE_FUEL_TYPE:
            const fuelValue = action.payload;
            return {
                ...state,
                fuelType: state.fuelType.includes(fuelValue)
                    ? state.fuelType.filter((type) => type !== fuelValue) // Remove if exists
                    : [...state.fuelType, fuelValue] // Add if not exists
            };

        case SORT_PRODUCTS:
            const sortType = action.payload;
            return { ...state, sortType };

        case CLEAR_FILTERS:
            return {
                budget: {min: 0, max: Infinity},
                fuelType: [], // Array to store selected fuel types as integers
                sortType: '' // sorting based on price and kms - price-lh , price-hl, kms-lh, kms-hl
            };
        
        case CLEAR_BUDGET:
            return {
                ...state,
                budget: {min: 0, max: Infinity}
            };

        default:
            return state;
    }
};

export default filterReducer;
