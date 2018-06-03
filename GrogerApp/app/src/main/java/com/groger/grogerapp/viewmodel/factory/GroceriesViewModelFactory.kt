package com.groger.grogerapp.viewmodel.factory

import android.arch.lifecycle.ViewModel
import android.arch.lifecycle.ViewModelProvider
import com.groger.grogerapp.service.model.Cluster
import com.groger.grogerapp.viewmodel.GroceriesViewModel

class GroceriesViewModelFactory(private val token: String, private val cluster: Cluster) : ViewModelProvider.Factory {

    override fun <T : ViewModel?> create(modelClass: Class<T>): T {

        if (modelClass.isAssignableFrom(GroceriesViewModel::class.java)) {
            val vm = GroceriesViewModel(token, cluster)
            return  vm as T
        }

        throw IllegalArgumentException("Unknown ViewModel Class")
    }

}