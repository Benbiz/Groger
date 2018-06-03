package com.groger.grogerapp.service.repository

import com.groger.grogerapp.service.model.Cluster
import com.groger.grogerapp.service.model.NewCluster
import io.reactivex.Observable
import retrofit2.Call

object ClusterRepositoryProvider {
    fun provideClusterRepository() : ClusterRepository
    {
        return ClusterRepository(GrogerService.create())
    }
}

class ClusterRepository(private val apiService : GrogerService)
{
    fun getClusters(token: String) : Observable<List<Cluster>>
    {
        return Observable.create({
            val response = apiService.getClusters(token).execute()
            if (response.isSuccessful)
            {
                val token= response.body()
                if (token != null)
                    it.onNext(token)
                it.onComplete()
            }
            else
            {
                it.onError(Throwable (when (response?.code())
                {
                    404 -> "Service unreachable"
                    401 -> "Not authenticated"
                    500 -> "Service error"
                    else -> "Unknown error"
                }))
                it.onComplete()
            }
        })
    }

    fun addCluster(token : String, cluster: NewCluster) : Observable<Cluster>
    {
        return Observable.create({
            val response = apiService.addCluster(token, cluster).execute()
            if (response.isSuccessful)
            {
                val cluster= response.body()
                if (cluster != null)
                    it.onNext(cluster)
                it.onComplete()
            }
            else
            {
                it.onError(Throwable (when (response?.code())
                {
                    404 -> "Service unreachable"
                    401 -> "Not authenticated"
                    500 -> "Service error"
                    else -> "Unknown error"
                }))
                it.onComplete()
            }
        })
    }

    fun removeCluster(token : String, cluster: Cluster) : Observable<Unit>
    {
        return Observable.create({
            val response = apiService.removeCluster(token, cluster.id).execute()
            if (response.isSuccessful)
            {
                it.onComplete()
            }
            else
            {
                it.onError(Throwable (when (response?.code())
                {
                    404 -> "Service unreachable"
                    401 -> "Not authenticated"
                    500 -> "Service error"
                    else -> "Unknown error"
                }))
                it.onComplete()
            }
        })
    }
}