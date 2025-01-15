import React, { useEffect, useState } from 'react'
import './Budget.scss';
import { useDispatch, useSelector } from 'react-redux';
import { clearBudget, setBudget } from '../../actions/filterAction';



const Budget = () => {

    // TODO : Make this api call :
    // stg.carwale.com/api/stocks?budget=0-1
    // pass budget as query

    const budgetArray = [
        { min: 0, max: 3},
        { min: 3, max: 5},
        { min: 5, max: 8},
        { min: 8, max: 12},
        { min: 12, max: 20},
        { min: 20, max: null},
    ]

    const [activeIndex, setActiveIndex] = useState(null); 

    const dispatch = useDispatch();
    const budget = useSelector((state) => state.budget);

    const [minBudgetInput, setMinBudgetInput] = useState(0);
    const [maxBudgetInput, setMaxBudgetInput] = useState(21);

    const handleChange = (b, idx) => {
        if( activeIndex === idx ){
            dispatch(clearBudget())
            setActiveIndex(null);
            return ;
        }
        
        setActiveIndex(idx);
        dispatch(setBudget({min: b.min, max: b.max}))

        console.log("Budget : " , budget) 
    }   

    useEffect(() => {
        if(budget.min === 0 &&  budget.max === Infinity){
            setActiveIndex(null);
        }
    },[budget])


  return (
    <div>
        <h3>Budget(Lakh)</h3>

        <div className='wrapper-budget'>
        {
            budgetArray.map((b,idx) => {
                return (
                    <div 
                        key={idx} 
                        className={`budget-buttons flex-container ${ activeIndex === idx ? 'active' : ''}`}
                        onClick={() => handleChange(b,idx) }
                    >
                        ₹
                            {
                                (b.min !== 0) ? b.min : 'Below' 
                            }
                            
                            {
                                (b.max) ? '-'+b.max : '+'
                            }
                        Lakh
                    </div>
                )
            })
        }

            <div className='wrapper-budget-input'>
                <input 
                    className='bgt-ip' value={minBudgetInput} 
                    onChange={(e) => {
                        setMinBudgetInput(e.target.value)
                        dispatch(setBudget({min: e.target.value, max: ((budget.max && budget.max!==Infinity) ? budget.max : 21)}))} 
                    }
                    placeholder='0' type="number" name="min-budget" id="min-budget" 
                />
                -
                <input 
                    className='bgt-ip' value={maxBudgetInput} 
                    onChange={(e) => {
                        setMaxBudgetInput(e.target.value)
                        dispatch(setBudget({ min: minBudgetInput, max: e.target.value }))} 
                    } 
                    placeholder='21' type="number" name="max-budget" id="max-budget" 
                />
            </div>

        </div>
    </div>
  )
}

export default Budget