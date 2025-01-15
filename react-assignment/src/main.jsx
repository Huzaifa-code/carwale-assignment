import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import { FilterProvider } from './context/FilterContext.jsx'
import { Provider } from 'react-redux'
import store from './store';


createRoot(document.getElementById('root')).render(
  <StrictMode>
    <FilterProvider>
      <Provider store={store}>
        <App />
      </Provider>
    </FilterProvider>
  </StrictMode>,
)
