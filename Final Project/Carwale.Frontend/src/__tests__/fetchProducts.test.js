import { fetchProducts } from '../utils/fetchProducts';

global.fetch = vi.fn();

describe('fetchProducts', () => {
    afterEach(() => {
        vi.clearAllMocks();
    });

    it('should fetch products correctly with given filters', async () => {
        const mockData = {
            stocks: [
                { carName: 'Creta', priceNumeric: 10000, kmNumeric: 5000, fuel: 'Petrol', cityName: 'Mumbai' },
                { carName: 'Fronx', priceNumeric: 15000, kmNumeric: 2000, fuel: 'Diesel', cityName: 'Indore' },
                { carName: 'Fortuner', priceNumeric: 12000, kmNumeric: 3000, fuel: 'Petrol', cityName: 'Indore' },
                { carName: 'Venue', priceNumeric: 9000, kmNumeric: 10000, fuel: 'Diesel', cityName: 'Delhi' },
                { carName: 'Hilux', priceNumeric: 20000, kmNumeric: 1000, fuel: 'Electric', cityName: 'Mumbai' },
            ]
        };

        fetch.mockResolvedValueOnce({
            json: async () => mockData,
        });

        const fuelType = ['1', '2'];
        const budget = { min: 3, max: 10 };
        const sortType = 'price-lh';

        const products = await fetchProducts(fuelType, budget, sortType);

        expect(fetch).toHaveBeenCalledWith(
            'https://stg.carwale.com/api/stocks?fuel=1+2&budget=3-10'
        );
        expect(products).toEqual([
            { carName: 'Venue', priceNumeric: 9000, kmNumeric: 10000, fuel: 'Diesel', cityName: 'Delhi' },
            { carName: 'Creta', priceNumeric: 10000, kmNumeric: 5000, fuel: 'Petrol', cityName: 'Mumbai' },
            { carName: 'Fortuner', priceNumeric: 12000, kmNumeric: 3000, fuel: 'Petrol', cityName: 'Indore' },
            { carName: 'Fronx', priceNumeric: 15000, kmNumeric: 2000, fuel: 'Diesel', cityName: 'Indore' },
            { carName: 'Hilux', priceNumeric: 20000, kmNumeric: 1000, fuel: 'Electric', cityName: 'Mumbai' },
        ]);
    });

    it('should sort products by price in descending order', async () => {
        const mockData = {
            stocks: [
                { carName: 'Venue', priceNumeric: 10000, kmNumeric: 5000, fuel: 'Petrol', cityName: 'Delhi' },
                { carName: 'Fronx', priceNumeric: 15000, kmNumeric: 2000, fuel: 'Diesel', cityName: 'Indore' },
                { carName: 'Creta', priceNumeric: 12000, kmNumeric: 3000, fuel: 'Petrol', cityName: 'Mumbai' },
            ]
        };

        fetch.mockResolvedValueOnce({
            json: async () => mockData,
        });

        const fuelType = ['1', '2'];
        const budget = { min: 10, max: 20 };
        const sortType = 'price-hl';

        const products = await fetchProducts(fuelType, budget, sortType);

        expect(products).toEqual([
            { carName: 'Fronx', priceNumeric: 15000, kmNumeric: 2000, fuel: 'Diesel', cityName: 'Indore' },
            { carName: 'Creta', priceNumeric: 12000, kmNumeric: 3000, fuel: 'Petrol', cityName: 'Mumbai' },
            { carName: 'Venue', priceNumeric: 10000, kmNumeric: 5000, fuel: 'Petrol', cityName: 'Delhi' },
        ]);
    });

    it('should handle errors gracefully', async () => {
        fetch.mockRejectedValueOnce(new Error('Network Error'));

        await expect(fetchProducts([], null, null)).rejects.toThrow('Network Error');
    });
});
