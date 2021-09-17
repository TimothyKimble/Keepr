import { AppState } from '../AppState'
import { api } from './AxiosService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Notifier'
import { router } from '../router'
import $ from 'jquery'

class KeepsService {
  async getKeeps() {
    try {
      const res = await api.get('api/keeps')
      logger.log(res.data)
      AppState.keeps = res.data
    } catch (error) {
      logger.error("Couldn't get Keeps", error)
    }
  }

  async getKeepById(id) {
    try {
      const res = await api.get('api/keeps/' + id)
      logger.log(res.data)
      AppState.keeps.push(res.data)
      return res.data
    } catch (error) {
      logger.error("Couldn't get Keep", error)
    }
  }

  async createKeep(newKeep) {
    try {
      const res = await api.post('api/keeps', newKeep)
      AppState.keeps.push(res.data)
      return res.data
    } catch (error) {
      logger.error("Couldn't Post keep", error)
    }
  }

  async increaseViews(keep) {
    try {
      if (AppState.account !== keep.creator.id) {
        keep.views++
        const res = await api.put('api/keeps/' + keep.id, keep)
        logger.log(res.data)
        keep = res.data
      }
      return keep
    } catch (error) {
      logger.log(error)
    }
  }

  async deleteKeep(keepId) {
    try {
      if (await Pop.confirm()) {
        await api.delete('api/keeps/' + keepId)
        $('#modal' + keepId).modal('hide')
        router.push('/')
        Pop.toast('Deleted Keep!', 'success')
      }
    } catch (error) {
      logger.error("Couldn't Delete Keep", error)
    }
  }
}

export const keepsService = new KeepsService()
