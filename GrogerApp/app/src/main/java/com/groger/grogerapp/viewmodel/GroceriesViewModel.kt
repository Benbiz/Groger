package com.groger.grogerapp.viewmodel

import android.arch.lifecycle.MutableLiveData
import android.arch.lifecycle.ViewModel
import android.databinding.ObservableField
import com.groger.grogerapp.service.model.Cluster
import com.groger.grogerapp.service.model.Grocery
import com.groger.grogerapp.service.repository.GroceryRepositoryProvider
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.disposables.CompositeDisposable
import io.reactivex.observers.DisposableObserver
import io.reactivex.schedulers.Schedulers

class GroceriesViewModel(private val token : String, private val cluster : Cluster) : ViewModel() {

    val isLoading = ObservableField<Boolean>(false)
    val groceries = MutableLiveData<List<Grocery>>()
    private val disposables = CompositeDisposable()

    /*fun removeGrocery(grocery: Grocery)
    {
        isLoading.set(true)
        disposables.add(GroceryRepositoryProvider.provideGroceryRepository().removeGrocery(token, cluster).subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeWith(object : DisposableObserver<Unit>() {
                    override fun onComplete() {
                        isLoading.set(false)
                        loadClusters()
                    }

                    override fun onNext(t: Unit) {

                    }


                    override fun onError(e: Throwable) {
                        isLoading.set(false)
                        val reason = e.message
                    }

                }))
    }*/

    /*fun addCluster(cluster: NewCluster)
    {
        isLoading.set(true)
        disposables.add(ClusterRepositoryProvider.provideClusterRepository().addCluster(token, cluster).subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeWith(object : DisposableObserver<Cluster>() {
                    override fun onComplete() {
                        isLoading.set(false)
                        loadClusters()
                    }

                    override fun onNext(t: Cluster) {

                    }


                    override fun onError(e: Throwable) {
                        isLoading.set(false)
                        val reason = e.message
                    }

                }))
    }*/

    fun loadClusters()
    {
        isLoading.set(true)
        disposables.add(GroceryRepositoryProvider.provideGroceryRepository().getClusterGroceries(token, cluster).subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeWith(object : DisposableObserver<List<Grocery>>() {
                    override fun onComplete() {
                        isLoading.set(false)
                    }

                    override fun onNext(t: List<Grocery>) {
                        groceries.value = t
                    }


                    override fun onError(e: Throwable) {
                        isLoading.set(false)
                        val reason = e.message
                    }
                }))
    }
}