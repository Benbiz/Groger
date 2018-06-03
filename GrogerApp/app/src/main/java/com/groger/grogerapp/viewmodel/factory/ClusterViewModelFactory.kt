package com.groger.grogerapp.viewmodel.factory

import android.arch.lifecycle.ViewModel
import android.arch.lifecycle.ViewModelProvider
import com.groger.grogerapp.viewmodel.ClusterViewModel

class ClusterViewModelFactory(private val token: String) : ViewModelProvider.Factory {

    override fun <T : ViewModel?> create(modelClass: Class<T>): T {

        if (modelClass.isAssignableFrom(ClusterViewModel::class.java)) {
            return ClusterViewModel(token) as T
        }

        throw IllegalArgumentException("Unknown ViewModel Class")
    }

}