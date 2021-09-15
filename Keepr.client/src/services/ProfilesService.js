import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfilesService {
  async getProfile(id) {
    const res = await api.get('api/profiles/' + id)
    logger.log(res.data)
    AppState.activeProfile = res.data
    return AppState.activeProfile
  }

  async getProfileKeeps(id) {
    const res = await api.get('api/profiles/' + id + '/keeps')
    AppState.keeps = res.data
    return AppState.keeps
  }

  async getProfileVaults(id) {
    const res = await api.get('api/profiles/' + id + '/vaults')
    AppState.vaults = res.data
    return AppState.vaults
  }
}

export const profilesService = new ProfilesService()
