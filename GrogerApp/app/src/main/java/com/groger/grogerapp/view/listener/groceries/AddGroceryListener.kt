package com.groger.grogerapp.view.listener.groceries

import com.groger.grogerapp.service.model.AddGrocery
import com.groger.grogerapp.service.model.Grocery

interface AddGroceryListener {
    fun onAddGrocery(addedGrocery: AddGrocery)
}