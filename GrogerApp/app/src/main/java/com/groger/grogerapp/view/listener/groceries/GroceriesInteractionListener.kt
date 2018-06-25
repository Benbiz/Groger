package com.groger.grogerapp.view.listener.groceries

import com.groger.grogerapp.service.model.Grocery

interface GroceriesInteractionListener {
    fun onRemoveGrocery(grocery: Grocery)
    fun onGrocerySelected(grocery: Grocery)
}