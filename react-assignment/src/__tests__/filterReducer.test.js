import filterReducer from '../reducers/filterReducer';
import { SET_BUDGET, TOGGLE_FUEL_TYPE, CLEAR_FILTERS, SORT_PRODUCTS, CLEAR_BUDGET } from '../actions/filterAction.js';

describe('filterReducer', () => {
    it('This test should return the initial state when no action is passed', () => {
        const initialState = {
            budget: { min: 0, max: Infinity },
            fuelType: [],
            sortType: ''
        };

        expect(filterReducer(undefined, {})).toEqual(initialState);
    });

    it('This test should handle SET_BUDGET', () => {
        const action = {
            type: SET_BUDGET,
            payload: { min: 5000, max: 15000 }
        };
        const expectedState = {
            budget: { min: 5000, max: 15000 },
            fuelType: [],
            sortType: ''
        };

        expect(filterReducer(undefined, action)).toEqual(expectedState);
    });

    it('This test should handle TOGGLE_FUEL_TYPE (add fuel)', () => {
        const action = {
            type: TOGGLE_FUEL_TYPE,
            payload: 1 
        };
        const initialState = {
            budget: { min: 0, max: Infinity },
            fuelType: [],
            sortType: ''
        };
        const expectedState = {
            budget: { min: 0, max: Infinity },
            fuelType: [1],
            sortType: ''
        };

        expect(filterReducer(initialState, action)).toEqual(expectedState);
    });

    it('This test should handle TOGGLE_FUEL_TYPE (remove fuel)', () => {
        const action = {
            type: TOGGLE_FUEL_TYPE,
            payload: 1 
        };
        const initialState = {
            budget: { min: 0, max: Infinity },
            fuelType: [1],
            sortType: ''
        };
        const expectedState = {
            budget: { min: 0, max: Infinity },
            fuelType: [],
            sortType: ''
        };

        expect(filterReducer(initialState, action)).toEqual(expectedState);
    });

    it('This test should handle SORT_PRODUCTS', () => {
        const action = {
            type: SORT_PRODUCTS,
            payload: 'price-lh' 
        };
        const expectedState = {
            budget: { min: 0, max: Infinity },
            fuelType: [],
            sortType: 'price-lh'
        };

        expect(filterReducer(undefined, action)).toEqual(expectedState);
    });

    it('This test should handle CLEAR_FILTERS', () => {
        const action = {
            type: CLEAR_FILTERS
        };
        const initialState = {
            budget: { min: 5000, max: 15000 },
            fuelType: [1],
            sortType: 'price-lh'
        };
        const expectedState = {
            budget: { min: 0, max: Infinity },
            fuelType: [],
            sortType: ''
        };

        expect(filterReducer(initialState, action)).toEqual(expectedState);
    });

    it('This test should handle CLEAR_BUDGET', () => {
        const action = {
            type: CLEAR_BUDGET
        };
        const initialState = {
            budget: { min: 5000, max: 15000 },
            fuelType: [1],
            sortType: 'price-lh'
        };
        const expectedState = {
            budget: { min: 0, max: Infinity },
            fuelType: [1],
            sortType: 'price-lh'
        };

        expect(filterReducer(initialState, action)).toEqual(expectedState);
    });
});
