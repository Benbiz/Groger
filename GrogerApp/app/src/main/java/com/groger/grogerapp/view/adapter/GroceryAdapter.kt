package com.groger.grogerapp.view.adapter

import android.support.v7.widget.RecyclerView
import android.view.LayoutInflater
import android.view.ViewGroup
import com.bumptech.glide.Glide
import com.groger.grogerapp.databinding.GroceryRowBinding
import com.groger.grogerapp.service.model.Grocery
import com.groger.grogerapp.view.listener.groceries.GroceriesInteractionListener

class GroceryAdapter(private var groceries: List<Grocery>, private var listener: GroceriesInteractionListener) : RecyclerView.Adapter<GroceryAdapter.GroceryViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): GroceryViewHolder {
        val layoutInflater = LayoutInflater.from(parent.context)
        val binding = GroceryRowBinding.inflate(layoutInflater, parent, false)
        return GroceryViewHolder(binding)
    }

    override fun getItemCount(): Int = groceries.size

    override fun onBindViewHolder(holder: GroceryViewHolder, position: Int) {
        holder.bind(groceries[position], listener)
    }

    fun replaceData(arrayList: List<Grocery>) {
        groceries = arrayList
        notifyDataSetChanged()
    }

    class GroceryViewHolder(private val binding: GroceryRowBinding) : RecyclerView.ViewHolder(binding.root)
    {
        fun bind(grocery : Grocery, listener: GroceriesInteractionListener?)
        {
            binding.grocery = grocery
            if (grocery.picture != null)
                Glide.with(binding.root)
                        .load(grocery.picture)
                        .into(binding.imgGrocery)
            if (listener != null) {
                binding.root.setOnClickListener { listener.onGrocerySelected(grocery) }
                binding.root.setOnLongClickListener {
                    listener.onRemoveGrocery(grocery)
                    true
                }
            }
            binding.executePendingBindings()
        }
    }
}