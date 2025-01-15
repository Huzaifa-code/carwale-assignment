import { useEffect } from 'react'
import './App.css'
import { AppliedFilters, Filters, Products } from './components'

function App() {



  return (
    <div style={{paddingRight: '150px', paddingLeft: '150px'}}>
      <h1>Carwale Used Cars</h1>

      <p>With 10496 used cars in Mumbai from various models such as City, Wagon R, Creta, Ertiga, Baleno, E-Class, Grand i10 etc. ranging from Rs. 30,000 to Rs. 16 Crore, CarWale offers you a great choice and value for your money on your used car purchase. There are a total of 40 used car brands available like...</p>

      <div style={{display: 'grid', gridTemplateColumns: '1fr 2fr', gap: '1rem'}} className='main-container' >
        <Filters/>
        <div>
          <AppliedFilters/>
          <Products/>
        </div>
      </div>

    </div>
  )
}

export default App
