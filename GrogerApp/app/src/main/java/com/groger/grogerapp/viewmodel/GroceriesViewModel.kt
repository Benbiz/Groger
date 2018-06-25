package com.groger.grogerapp.viewmodel

import android.arch.lifecycle.MutableLiveData
import android.arch.lifecycle.ViewModel
import android.databinding.ObservableField
import com.groger.grogerapp.service.model.*
import com.groger.grogerapp.service.repository.GroceryRepositoryProvider
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.disposables.CompositeDisposable
import io.reactivex.observers.DisposableObserver
import io.reactivex.schedulers.Schedulers

class GroceriesViewModel(private val token : String, private val cluster : Cluster) : ViewModel() {

    val isLoading = ObservableField<Boolean>(false)
    val groceries = MutableLiveData<List<Grocery>>()
    private val disposables = CompositeDisposable()

    fun loadGroceries(query : String)
    {
        isLoading.set(true)
        disposables.add(GroceryRepositoryProvider.provideGroceryRepository().getGroceries(token, query).subscribeOn(Schedulers.newThread())
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

    fun createGrocery(grocery : NewGrocery)
    {
        isLoading.set(true)
        disposables.add(GroceryRepositoryProvider.provideGroceryRepository().createGrocery(token, cluster, grocery).subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeWith(object : DisposableObserver<Grocery>() {
                    override fun onComplete() {
                        isLoading.set(false)
                    }

                    override fun onNext(t: Grocery) {
                    }


                    override fun onError(e: Throwable) {
                        isLoading.set(false)
                        val reason = e.message
                    }
                }))
    }

    fun addGrocery(groceryId: Int,grocery: AddGrocery)
    {
        isLoading.set(true)
        disposables.add(GroceryRepositoryProvider.provideGroceryRepository().addGrocery(token, cluster, groceryId, grocery).subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeWith(object : DisposableObserver<Grocery>() {
                    override fun onComplete() {
                        isLoading.set(false)
                    }

                    override fun onNext(t: Grocery) {

                    }


                    override fun onError(e: Throwable) {
                        isLoading.set(false)
                        val reason = e.message
                    }
                }))
    }

    fun loadClusterGroceries()
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

    fun removeGrocery(grocery: Grocery)
    {
        isLoading.set(true)
        disposables.add(GroceryRepositoryProvider.provideGroceryRepository().removeGrocery(token, cluster, grocery).subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeWith(object : DisposableObserver<Unit>() {
                    override fun onComplete() {
                        isLoading.set(false)
                        loadClusterGroceries()
                    }

                    override fun onNext(t: Unit) {
                    }


                    override fun onError(e: Throwable) {
                        isLoading.set(false)
                        val reason = e.message
                    }
                }))
    }

    fun updateGrocery(groceryId: Int, grocery: UpdateGrocery)
    {
        isLoading.set(true)
        disposables.add(GroceryRepositoryProvider.provideGroceryRepository().updateClusterGroceries(token, cluster, groceryId, grocery).subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeWith(object : DisposableObserver<Unit>() {
                    override fun onComplete() {
                        isLoading.set(false)
                    }

                    override fun onNext(t: Unit) {
                    }


                    override fun onError(e: Throwable) {
                        isLoading.set(false)
                        val reason = e.message
                    }
                }))
    }

    override fun onCleared() {
        disposables.dispose()
        super.onCleared()
    }
}