// utils/fetchProducts.js
export const fetchProducts = async (fuelType = [], budget = null, sortType = null) => {
    const fuelParam = fuelType.length > 0 ? fuelType.join('+') : '';
    const budgetParam = budget ? `${budget.min}-${budget.max ?? ''}` : '';
    const url = `https://stg.carwale.com/api/stocks?fuel=${fuelParam}&budget=${budgetParam}`;

    try {
        const response = await fetch(url);
        const data = await response.json();

        let sortedData = data.stocks || [];
        if (sortType) {
            switch (sortType) {
                case 'price-lh':
                    sortedData.sort((a, b) => a.priceNumeric - b.priceNumeric);
                    break;
                case 'price-hl':
                    sortedData.sort((a, b) => b.priceNumeric - a.priceNumeric);
                    break;
                case 'kms-lh':
                    sortedData.sort((a, b) => a.kmNumeric - b.kmNumeric);
                    break;
                case 'kms-hl':
                    sortedData.sort((a, b) => b.kmNumeric - a.kmNumeric);
                    break;
                default:
                    break;
            }
        }
        return sortedData;
    } catch (error) {
        console.error('Failed to fetch products:', error);
        throw error;
    }
};
