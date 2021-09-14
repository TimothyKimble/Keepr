import { AppState } from '../AppState'
import { api } from './AxiosService'
import { logger } from '../utils/Logger'

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

  async createKeep(newKeep) {
    try {
      const res = await api.post('api/keeps', newKeep)
      AppState.keeps.push(res.data)
    } catch (error) {
      logger.error("Couldn't Post keep", error)
    }
  }
}

export const keepsService = new KeepsService()
