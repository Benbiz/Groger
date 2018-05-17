package com.groger.grogerapp.view.adapter

import android.support.v7.widget.RecyclerView
import android.view.LayoutInflater
import android.view.ViewGroup
import com.groger.grogerapp.databinding.ClusterRowBinding
import com.groger.grogerapp.service.model.Cluster

class ClusterAdapter(private var clusters: List<Cluster>, private var listener: OnItemClickListener) : RecyclerView.Adapter<ClusterAdapter.ClusterViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ClusterViewHolder {
        val layoutInflater = LayoutInflater.from(parent.context)
        val binding = ClusterRowBinding.inflate(layoutInflater, parent, false)
        return ClusterViewHolder(binding)
    }

    override fun getItemCount(): Int = clusters.size

    interface OnItemClickListener{
        fun onItemClick(position: Int)
        fun onItemLongClick(position: Int)
    }

    override fun onBindViewHolder(holder: ClusterViewHolder, position: Int) {
        holder.bind(clusters[position], listener)
    }

    fun replaceData(arrayList: List<Cluster>) {
        clusters = arrayList
        notifyDataSetChanged()
    }

    class ClusterViewHolder(private val binding: ClusterRowBinding) : RecyclerView.ViewHolder(binding.root)
    {
        fun bind(cluster : Cluster, listener: OnItemClickListener?)
        {
            binding.cluster = cluster
            if (listener != null) {
                binding.root.setOnClickListener { listener.onItemClick(layoutPosition) }
                binding.root.setOnLongClickListener {
                    listener.onItemLongClick(layoutPosition)
                    true
                }
            }
            binding.executePendingBindings()
        }
    }
}