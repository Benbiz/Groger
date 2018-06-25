package com.groger.grogerapp.viewmodel

import android.arch.lifecycle.MutableLiveData
import android.arch.lifecycle.ViewModel
import android.databinding.ObservableField
import com.groger.grogerapp.service.model.Cluster
import com.groger.grogerapp.service.model.NewCluster
import com.groger.grogerapp.service.repository.ClusterRepositoryProvider
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.disposables.CompositeDisposable
import io.reactivex.observers.DisposableObserver
import io.reactivex.schedulers.Schedulers

class ClusterViewModel(private val token : String) : ViewModel() {

    val isLoading = ObservableField<Boolean>(false)
    val clusters = MutableLiveData<List<Cluster>>()
    private val disposables = CompositeDisposable()

    fun removeCluster(cluster: Cluster)
    {
        isLoading.set(true)
        disposables.add(ClusterRepositoryProvider.provideClusterRepository().removeCluster(token, cluster).subscribeOn(Schedulers.newThread())
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
    }

    fun addCluster(cluster: NewCluster)
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
    }

    fun loadClusters()
    {
        isLoading.set(true)
        disposables.add(ClusterRepositoryProvider.provideClusterRepository().getClusters(token).subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeWith(object : DisposableObserver<List<Cluster>>() {
                    override fun onComplete() {
                        isLoading.set(false)
                    }

                    override fun onNext(t: List<Cluster>) {
                        clusters.value = t
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